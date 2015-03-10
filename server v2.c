/*
compile :  gcc server.c -o server -lpthread `mysql_config --cflags --libs`
*/

#include<stdio.h>
#include<string.h>
#include<stdlib.h>
#include<sys/socket.h>
#include<arpa/inet.h> //inet_addr
#include<unistd.h> //write
#include<pthread.h>
#include<mysql/mysql.h>

//thread function
void *connection_handler(void *);

struct NODE {
	int sock;
	char username[1024];
	struct NODE *next;
};
typedef struct NODE list;

list *useronline;

int splitstr(char src[1024], char c, char dst[1024][1024]){
	char temp = src[0];
	src[strlen(src)]='\0';
	int i=0,x=0,y=0;
	while(src[i] != '\0'){
		if(src[i] != c){
			dst[x][y] = src[i];
			y++;
		}
		else if (src[i] == c){
			x++;
			y = 0;
		}
		i++;
	}
}
void append_node(list **llist, int sock, char username[1024]) {
	list *first = *llist;
	if(*llist == NULL){
		*llist = (list *)malloc(sizeof(list));
		(*llist)->sock = sock;
		strcpy((*llist)->username,username);
		(*llist)->next = NULL;
	}
	else{
		while(first->next != NULL)
			first = first->next;
		first->next = (list *)malloc(sizeof(list));
		first->next->sock = sock;
		strcpy(first->next->username,username);
		first->next->next = NULL;
	}
}
void delete_node(list **llist, int sock, char username[1024]) {
	list *pointer = *llist;
	list *temp;
	temp = (list *)malloc(sizeof(list));
	if(pointer->sock == sock){
		temp = *llist;
		*llist = (*llist)->next;
		free(temp);
		return;
	}
	while(pointer->next->sock != sock){
		pointer = pointer->next;
		if(pointer->next == NULL) return;
	}
	temp = pointer->next;
	pointer->next = pointer->next->next;
	free(temp);
}


int main(int argc , char *argv[]){
	int socket_desc , client_sock , c;
	struct sockaddr_in server , client;
	useronline = NULL;

	//Create socket
	socket_desc = socket(AF_INET , SOCK_STREAM , 0);
	if (socket_desc == -1)
	{
		printf("Could not create socket");
	}
	puts("Socket created");
	
	//Prepare the sockaddr_in structure
	server.sin_family = AF_INET;
	server.sin_addr.s_addr = INADDR_ANY;
	server.sin_port = htons( 8888 );
	
	//Bind
	int optval = 1;
	setsockopt(socket_desc,SOL_SOCKET, SO_REUSEPORT, &optval, sizeof(optval));
	if( bind(socket_desc,(struct sockaddr *)&server , sizeof(server)) < 0)
	{
		//print the error message
		perror("bind failed. Error");
		return 1;
	}
	puts("bind done");
	listen(socket_desc , 3);
	
	//Accept and incoming connection
	puts("Waiting for incoming connections...");
	c = sizeof(struct sockaddr_in);
	pthread_t thread_id;

	/*
	if( pthread_create( &thread_id , NULL , updateonlineuser , (void*) NULL ) < 0){
		perror("could not create thread updateonlineuser");
		return 1;
	}*/
	
	while( (client_sock = accept(socket_desc, (struct sockaddr *)&client, (socklen_t*)&c)) )
	{
		puts("Connection accepted");
		if( pthread_create( &thread_id , NULL , connection_handler , (void*) &client_sock) < 0)
		{
			perror("could not create thread connection_handler");
			return 1;
		}
		//Now join the thread , so that we dont terminate before the thread
		//pthread_join( thread_id , NULL);
		puts("Handler assigned");
	}
	if (client_sock < 0)
	{
		perror("accept failed");
		return 1;
	}
	return 0;
}

void *connection_handler(void *socket_desc){
	//Get the socket descriptor
	int sock = *(int*)socket_desc;
	int read_size;
	char client_message[2000];

	//Receive
	while( (read_size = recv(sock , client_message , 2000 , 0)) > 0 ){
		//end of string
		client_message[read_size] = '\0';
		//printf("%s",client_message);
		//get command
		
		char message[1024][1024];
		char command[1024];
		splitstr(client_message,' ',message);
		strcpy(command,message[0]);

		////process command
		if(!strcmp(command,"signup")){
			char lala;
		}
		
		else if(!strcmp(command,"login")){
			char username[1001];
			strcpy(username,message[1]);

			int isSama = 0;

			//cari ada username yang sama nggak.
			list *it = useronline;
			while(1){
				if(it==NULL) break;
				if( !strcmp(it->username,username) ){
					isSama = 1;
					break;
				}
				it = it->next;
			}

			if(isSama == 0){
				append_node(&useronline,sock,username);
				send(sock , "login ok" , strlen("login ok"),0);
			}
			else{
				send(sock , "login failed, exsist username" , strlen("login failed, exsist username"),0);
			}
		}
		
		else if(!strcmp(command,"message")){
			//cari ada username yang sama nggak.
			char usertujuan[1001];
			strcpy(usertujuan,message[2]);
			int isFound = 0;

			list *it = useronline;
			while(1){
				if(it==NULL) break;
				if( !strcmp(it->username,usertujuan) ){
					isFound = 1;
					break;
				}
				it = it->next;
			}
			if(isFound){
				send(it->sock,client_message,strlen(client_message),0);
				send(sock, "message ok",strlen("message ok"),0);
			}
			else{
				send(sock , "message Could not send message" , strlen("message Could not send message"),0);
			}
		}
		else if(!strcmp(command,"ping")){
			char listonlineuser[10001];
			strcpy(listonlineuser,"online\n");
			ist *it = useronline;

			while(1){
				if(it==NULL) break;
				strcat(listonlineuser,it->username);
				strcat(listonlineuser,"\n");
				it = it->next;
			}
			send(sock, listonlineuser,strlen(listonlineuser),0);
		}
		else{
			send(sock , "unknown" , strlen("unknown"),0);
		}
		//*/

//Send
		//send(sock , client_message , strlen(client_message),0);
//clear the message buffer
		memset(command, 0, 2000);
		memset(client_message, 0, 2000);
		memset(message, 0, 2000);
	}
	if(read_size == 0)
	{
		//update seen user
		puts("Client disconnected");
		fflush(stdout);
	}
	else if(read_size == -1)
	{
		perror("recv failed");
	}
	return 0;
} 

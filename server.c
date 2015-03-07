/*
compile :  gcc server.c -lpthread -o server
*/

#include<stdio.h>
#include<string.h>
#include<stdlib.h>
#include<sys/socket.h>
#include<arpa/inet.h> //inet_addr
#include<unistd.h> //write
#include<pthread.h>
//thread function
void *connection_handler(void *);
int main(int argc , char *argv[])
{
	int socket_desc , client_sock , c;
	struct sockaddr_in server , client;
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
	while( (client_sock = accept(socket_desc, (struct sockaddr *)&client, (socklen_t*)&c)) )
	{
		puts("Connection accepted");
		if( pthread_create( &thread_id , NULL , connection_handler , (void*) &client_sock) < 0)
		{
			perror("could not create thread");
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
void *connection_handler(void *socket_desc)
{
//Get the socket descriptor
	int sock = *(int*)socket_desc;
	int read_size;
	char *message , client_message[2000];

//Receive
	while( (read_size = recv(sock , client_message , 2000 , 0)) > 0 )
	{
//end of string
		client_message[read_size] = '\0';

		//get command
		char command[1024];
		int i;
		for(i=0;;i++){
			if(client_message[i]==' ')break;
			command[i] = client_message[i];
		}

		//process command
		if(!strcmp(command,"signup")){
			//cek di db ada username yang sama atau nggak
			//klo ada yg sama send "signup exsist"
			//klo nggak ada insert ke db, send "signup ok"
		}
		else if(!strcmp(command,"login")){
			//cek username n pass di db
			//klo ada 
				//send "login ok"
				//update seen user di db
				//query db message yg tujuannya ke dia, kirim semua
				//kirim list online user
			//klo nggak ada
				//send "login wrong"
		}
		else if(!strcmp(command,"message")){
			//get from, to, id, body
			//insert pesan ke db
			//cek online user, klo online, dapetin socketnya, kirim pesan ke tujuan
			//send "message ok"

		}
		else if(!strcmp(command,"ping")){
			//update seen user di db
			
		}
		else if(!strcmp(command,"delivered_message")){
			//dapetin id message
			//query db dg id message, dapetin pengirimnya
			//buat pesan ("delivered_message <id>") dg pk d_<id> tujuan ke pengirim dari server, insert ke db
			//cek online user, klo online, dapetin socketnya, kirim pesan
		}
		else if(!strcmp(command,"delivered_info")){
			//dapetin id
			//hapus pesan di db dengan pk d_<id>
		}

//Send
		write(sock , client_message , strlen(client_message));
//clear the message buffer
		memset(client_message, 0, 2000);
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

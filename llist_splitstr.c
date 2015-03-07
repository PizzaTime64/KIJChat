#include<stdio.h>
#include<stdlib.h>
#include<string.h>

struct NODE {
 int sock;
 char username[1024];
 struct NODE *next;
};

typedef struct NODE list;

void append_node(list **llist, int sock, char username[1024]) {
	list *first = *llist;
	while(first->next != NULL)
	first = first->next;

	first->next = (list *)malloc(sizeof(list));
	first->next->sock = sock;
	strcpy(first->next->username,username);
	first->next->next = NULL;
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

void display(list **llist){
	list *pointer = *llist;
	while(pointer->next != NULL){
		printf("%d ",pointer->sock);
		pointer = pointer->next;
	}
	printf("%d ",pointer->sock);
}

void newlist(list ** llist){
	*llist = (list *)malloc(sizeof(list));
	(*llist)->sock = 0;
	strcpy((*llist)->username,"");
 	(*llist)->next = NULL;
}

int split(char c, char dst[1024][1024], char src[1024]){
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

int main(){
	list *llist;
	newlist(&llist); 	
 	append_node(&llist,1,"aaa");
 	append_node(&llist,2,"aaa");
 	append_node(&llist,3,"aaa");
 	delete_node(&llist,3,"aaa");
	append_node(&llist,4,"aaa");
	display(&llist);
	printf("\n");
	
}

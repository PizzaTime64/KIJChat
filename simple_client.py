import socket
import sys
import threading
import select

client_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server_address = ('192.168.1.111',8888)
client_socket.connect(server_address)

#recv = client_socket.recv(1024)
#print recv
while True:
	socket_list = [sys.stdin,client_socket]
	readready, writeready, error = select.select(socket_list, [], [])

	for sock in readready:
		if sock == client_socket:
			data = sock.recv(2048)
			if not data:
				print ("Disconnected from server")
				sys.exit()
			else:
				print (data)
		else:
			msg = sys.stdin.readline()
			client_socket.send(msg)

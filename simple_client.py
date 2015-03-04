import socket
import sys

client_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server_address = ('127.0.0.1',8888)
client_socket.connect(server_address)

recv = client_socket.recv(1024)
print recv
client_socket.send("hello")
recv = client_socket.recv(1024)
print recv

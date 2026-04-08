import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root',
})
export class chatService {
  private hubConnection!:signalR.HubConnection
  public messages: { user: string, text: string, sentiment?: string }[] = [];
 startConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://chatapplication-h0bjcnfjexchhabq.polandcentral-01.azurewebsites.net//chat') // backend URL
      .withAutomaticReconnect()
      .build();

    this.hubConnection.start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error: ', err));
  }

  addReceiveListener() {
    this.hubConnection.on('ReceiveMessage', (user, message, sentiment) => {
      this.messages.push({ user, text: message, sentiment });
    });
  }

  sendMessage(user: string, message: string) {
    this.hubConnection.invoke('SendMessage', user, message);
  }
}

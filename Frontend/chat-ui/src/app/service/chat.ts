import { Injectable, NgZone } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class chatService {
  private hubConnection!:signalR.HubConnection
  public messages: { user: string, text: string, sentiment?: string; createdAtUtc?: string }[] = [];

  constructor(private ngZone: NgZone) {}
 startConnection() {
  if (this.hubConnection) {
      return;
    }
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(environment.hubUrl, {
    withCredentials: true
  })
      .withAutomaticReconnect()
      .build();

    this.registerListeners();

    this.hubConnection.start()
      .then(() => {
        console.log('Connection started');
        return this.hubConnection.invoke('LoadHistory');
      }).catch(err => console.log('Error: ', err));
  }

   private registerListeners(): void {
    this.hubConnection.on('ReceiveMessage', (user, message, sentiment, createdAtUtc) => {
      this.ngZone.run(() => {
        this.messages.push({
          user,
          text: message,
          sentiment,
          createdAtUtc,
        });
      });
    });


    this.hubConnection.on('ReceiveHistory', (messages) => {
      this.ngZone.run(() => {
        console.log('History received:', messages);
        this.messages = messages ?? [];
      });
    });
  }

  sendMessage(user: string, message: string): void {
    if (!this.hubConnection) {
      return;
    }

    this.hubConnection
      .invoke('SendMessage', user, message)
      .catch((err) => console.log('Send error:', err));
  }
}

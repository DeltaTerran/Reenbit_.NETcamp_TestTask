import { Injectable, NgZone, signal } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { environment } from '../../environments/environment.development';
export interface ChatMessage {
  user: string;
  text: string;
  sentiment?: string;
  createdAtUtc?: string;
}
@Injectable({
  providedIn: 'root',
})
export class chatService {
  private hubConnection!:signalR.HubConnection
  public messages = signal<ChatMessage[]>([]);

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
        this.messages.update((current) => [
          ...current,
          {
            user,
            text: message,
            sentiment,
            createdAtUtc,
          },
        ]);
      });
    });

    this.hubConnection.on('ReceiveHistory', (messages) => {
      this.ngZone.run(() => {
        console.log('History received:', messages);
        this.messages.set(messages ?? []);
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

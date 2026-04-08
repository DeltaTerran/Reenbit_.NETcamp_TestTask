import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { chatService } from '../service/chat';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './chat.html',
  styleUrl: './chat.css',
})
export class Chat implements OnInit {
  user = '';
  message = '';

  constructor(public chatService: chatService) {}

  ngOnInit(): void {
    this.chatService.startConnection();
  }

  send(): void {
    if (!this.user.trim() || !this.message.trim()) {
      return;
    }

    this.chatService.sendMessage(this.user, this.message);
    this.message = '';
  }
}
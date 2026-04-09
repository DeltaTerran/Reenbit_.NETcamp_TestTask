import { Component, OnInit, AfterViewChecked, ElementRef, ViewChild   } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { chatService } from '../service/chat';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './chat.html',
  styleUrl: './chat.css',
})
export class Chat implements OnInit, AfterViewChecked {
  user = '';
  message = '';
  isNameSet = false;
@ViewChild('scrollContainer') private scrollContainer!: ElementRef;
  constructor(public chatService: chatService) {}

  ngOnInit(): void {
    const savedUser = localStorage.getItem('chat-user');

    if (savedUser && savedUser.trim()) {
      this.user = savedUser;
      this.isNameSet = true;
    this.chatService.startConnection();
  }  
}
 confirmUser(): void {
    if (!this.user.trim()) {
      return;
    }

    this.user = this.user.trim();
    localStorage.setItem('chat-user', this.user);
    this.isNameSet = true;
    this.chatService.startConnection();
  }
ngAfterViewChecked(): void {
    this.scrollToBottom();
  }
  private scrollToBottom(): void {
    try {
      const el = document.querySelector('.chat-messages');
      if (el) el.scrollTop = el.scrollHeight;
    } catch {}
  }
  send(): void {
    if (!this.user.trim() || !this.message.trim()) {
      return;
    }

    this.chatService.sendMessage(this.user, this.message);
    this.message = '';
  }
  
  changeUser(): void {
    localStorage.removeItem('chat-user');
    this.user = '';
    this.message = '';
    this.isNameSet = false;
  }
}
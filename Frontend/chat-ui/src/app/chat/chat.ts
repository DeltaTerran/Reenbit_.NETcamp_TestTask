import { Component, OnInit } from '@angular/core';
import { chatService } from '../service/chat';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-chat',
  imports: [FormsModule],
  templateUrl: './chat.html',
  styleUrl: './chat.css',
})
export class Chat implements OnInit {
user='';
message='';
  constructor(public chatService:chatService){}
ngOnInit(): void {
  this.chatService.startConnection();
  this.chatService.addReceiveListener();
}
send(){
  this.chatService.sendMessage(this.user,this.message);
  
}
}

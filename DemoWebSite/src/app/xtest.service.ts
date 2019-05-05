import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Injectable, EventEmitter } from '@angular/core'
import { Subject } from 'rxjs/Subject'

@Injectable()
export class XTestService {


    private todo = new BehaviorSubject<string>("");
    currentUser = this.todo.asObservable();

 
    constructor() {}

    SetFunction(t: string){
        this.todo.next(t)
    }
}
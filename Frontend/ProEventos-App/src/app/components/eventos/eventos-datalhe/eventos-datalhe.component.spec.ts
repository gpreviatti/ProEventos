import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventosDatalheComponent } from './eventos-datalhe.component';

describe('EventosDatalheComponent', () => {
  let component: EventosDatalheComponent;
  let fixture: ComponentFixture<EventosDatalheComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EventosDatalheComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EventosDatalheComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

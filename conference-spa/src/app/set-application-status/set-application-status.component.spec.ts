import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SetApplicationStatusComponent } from './set-application-status.component';

describe('SetApplicationStatusComponent', () => {
  let component: SetApplicationStatusComponent;
  let fixture: ComponentFixture<SetApplicationStatusComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SetApplicationStatusComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SetApplicationStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

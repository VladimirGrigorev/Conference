import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalSetApplicationStatusComponent } from './modal-set-application-status.component';

describe('ModalSetApplicationStatusComponent', () => {
  let component: ModalSetApplicationStatusComponent;
  let fixture: ComponentFixture<ModalSetApplicationStatusComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ModalSetApplicationStatusComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ModalSetApplicationStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

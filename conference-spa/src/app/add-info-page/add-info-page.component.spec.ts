import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddInfoPageComponent } from './add-info-page.component';

describe('AddInfoPageComponent', () => {
  let component: AddInfoPageComponent;
  let fixture: ComponentFixture<AddInfoPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddInfoPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddInfoPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

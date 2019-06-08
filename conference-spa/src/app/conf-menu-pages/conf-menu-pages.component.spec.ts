import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfMenuPagesComponent } from './conf-menu-pages.component';

describe('ConfMenuPagesComponent', () => {
  let component: ConfMenuPagesComponent;
  let fixture: ComponentFixture<ConfMenuPagesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConfMenuPagesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfMenuPagesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

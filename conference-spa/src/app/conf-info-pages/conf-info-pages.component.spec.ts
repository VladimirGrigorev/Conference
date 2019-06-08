import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfInfoPagesComponent } from './conf-info-pages.component';

describe('ConfInfoPagesComponent', () => {
  let component: ConfInfoPagesComponent;
  let fixture: ComponentFixture<ConfInfoPagesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConfInfoPagesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfInfoPagesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

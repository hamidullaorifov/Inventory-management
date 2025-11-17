import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomIdComponent } from './custom-id.component';

describe('CustomIdComponent', () => {
  let component: CustomIdComponent;
  let fixture: ComponentFixture<CustomIdComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomIdComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CustomIdComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

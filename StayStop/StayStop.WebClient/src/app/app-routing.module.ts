import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './guards/auth-guard.guard';
import { HotelsComponent } from './hotels/hotels.component';
import { DetailsComponent } from './hotels/details/details.component';
import { RoomDetailsComponent } from './hotels/room-details/room-details.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent},
  {path: 'hotels', component: HotelsComponent},
  {path: 'hotels/:hotelid', component: DetailsComponent},
  {path: `hotels/:hotelid/rooms/:roomid`,component: RoomDetailsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

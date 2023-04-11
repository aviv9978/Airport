import { Pilot } from './forFlight/Pilot';
import { Plane } from './forFlight/Plane';

export interface Flight {
  code: number;
  pilot: Pilot;
  plane: Plane;
}

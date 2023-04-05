import { Plane } from './ForFlight/Plane';
import { Pilot } from './ForFlight/pilot';

export interface Flight {
  Code: number;
  Pilot: Pilot;
  Plane: Plane;
}

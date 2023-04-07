import { Plane as Plain } from './ForFlight/Plane';
import { Pilot } from './ForFlight/pilot';

export interface Flight {
  code: number;
  pilot: Pilot;
  plain: Plain;
}

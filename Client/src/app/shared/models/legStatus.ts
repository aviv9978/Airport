import { Flight } from './Flight';
import { LegNumber } from './enums/legNumbers';
export interface LegStatus {
  legNumber: string;
  isOccupied: boolean;
  flight?: Flight;
}

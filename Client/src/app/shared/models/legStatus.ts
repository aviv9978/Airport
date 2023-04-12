import { Flight } from './Flight';
import { LegNumber } from './enums/legNumbers';
export interface LegStatus {
  legNumber: number;
  isOccupied: boolean;
  flight?: Flight;
}

export interface KeyValuePair {
  id: number;
  name: string;
}

export interface Contact {
  name: string;
  phone: string;
  email: string;
}

export interface VehicleModel {
  id: number;
  vehicle: KeyValuePair;
  manufacturer: KeyValuePair;
  isRegistered: boolean;
  features: KeyValuePair[];
  contact: Contact;
  lastUpdate: string;
}

export interface SaveVehicle {
  id: number;
  vehicleId: number;
  manufacturerId: number;
  isRegistered: boolean;
  features: number[];
  contact: Contact;
}



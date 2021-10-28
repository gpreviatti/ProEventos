import { Palestrante } from './Palestrante';

export interface RedeSocial {
  id: number;
  nome: string;
  url: string;
  palestranteId?: number;
  palestrante: Palestrante;
}

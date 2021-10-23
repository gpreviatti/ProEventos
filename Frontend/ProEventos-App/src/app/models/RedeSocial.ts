import { Palestrante } from './Palestrante';

export interface RedeSocial {
  id: number;
  nome: string;
  uRL: string;
  palestranteId?: number;
  palestrante: Palestrante;
}

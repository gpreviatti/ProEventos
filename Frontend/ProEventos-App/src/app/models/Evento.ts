import { Lote } from './Lote';
import { PalestranteEvento } from './PalestranteEvento';

export interface Evento {
  id: number;
  local: string;
  dataEvento?: Date;
  tema: string;
  qtdPessoas: number;
  imagemURL: string;
  telefone: string;
  email: string;
  lotes: Lote[];
  palestrantesEventos: PalestranteEvento;
}

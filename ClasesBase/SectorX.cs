﻿using System;

namespace ClasesBase
{
    public class SectorX : Sector
    {
        private string estado;
        private int? ultimoTicketReservado;

        public SectorX(int sectorCodigo, string descripcion, string identificador, bool habilitado, int zonaCodigo, string estado, int? ultimoTicketReservado)
            : base(sectorCodigo, descripcion, identificador, habilitado, zonaCodigo)
        {
            this.estado = estado;
            this.ultimoTicketReservado = ultimoTicketReservado;
        }

        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public int? UltimoTicketReservado
        {
            get { return ultimoTicketReservado; }
            set { ultimoTicketReservado = value; }
        }
    }
}
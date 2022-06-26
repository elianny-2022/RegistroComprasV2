using Microsoft.EntityFrameworkCore;
public class ComprasBLL
    {
        private Contexto _contexto;
        public ComprasBLL(Contexto contexto)
        {
            _contexto = contexto; 
        }
        public bool Guardar(Compras compra)
        {
            _contexto.Compras.Add(compra);

            foreach (var item in compra.Detalle)
            {
                var producto = _contexto.Productos.Find(item.ProductoId);

                producto.Existencia += item.Cantidad;

            }

            var insertados = _contexto.SaveChanges();

            return insertados > 0;
        }
        private bool Existe(int Id)
        {
            bool existe = false;

            try{
                existe = _contexto.Compras.Any(c => c.CompraId == Id);
            }catch (Exception e)
            {
                throw e;
            }
            return existe;
        }
        public  Compras? BuscarCompra(int Id)

        {
           return _contexto.Compras.Include(c => c.Detalle)
           .AsNoTracking()
           .FirstOrDefault(c => c.CompraId == Id);
        }
        
        public List<Compras> BuscarFecha( DateTime fecha, DateTime fecha1)
        {
            var fechas = _contexto.Compras
            .Where(f => f.Fecha.Date == fecha.Date || f.Fecha.Date == fecha1.Date)
            .AsNoTracking().ToList();
            return fechas;

        }
        public bool Eliminar(int Id){
            bool paso = false;
            try
            {
                var eliminar = _contexto.Compras.Find(Id);
                _contexto.Entry(eliminar).State = EntityState.Deleted;
                paso = (_contexto.SaveChanges() > 0);
            }
            catch (System.Exception)
            {
                
                throw;
            }
            return paso;
        }
       
         public List<Compras> GetList()
        {
           return _contexto.Compras.AsNoTracking().ToList();
        }
        
    }
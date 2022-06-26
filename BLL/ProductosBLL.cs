using Microsoft.EntityFrameworkCore;

public class ProductosBLL
    {
        private Contexto _contexto;
        public ProductosBLL(Contexto contexto)
        {
            _contexto = contexto; 
        }
       
        public Productos? Buscar(int Id)
        {
            var producto = _contexto.Productos
                .AsNoTracking()
                .FirstOrDefault(p => p.ProductoId == Id );

            return producto;
        }
        /* public  Productos? BuscarProducto(int Id)

        {
           return _contexto.Productos.Include(c => c.Costo)
           .AsNoTracking()
           .FirstOrDefault(c => c.ProductoId == Id);
        }*/
        public List<Productos> GetList()
        {
           return _contexto.Productos.AsNoTracking().ToList();
        }
    }
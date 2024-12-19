using DTO;
using Transportes_API_gen13.Models;

namespace Transportes_API_gen13.Services
{
    public interface ICamiones
    {
        //es una estructura que define un contrato o conjunto de métodos y
        //propiedades que una clase debe implementar.
        //Una interfaz establece un conjunto de requisitos que cualquier clase
        //que la implemente debe seguir. Estos requisitos son declarados en la
        //interfaz en forma de firmas de métodos y propiedades,
        //pero la interfaz en sí misma no proporciona ninguna implementación
        //de estos métodos o propiedades.Es responsabilidad de las clases que
        //implementan la interfaz proporcionar las implementaciones concretas de
        //estos miembros.

        //Las interfaces son útiles para lograr la abstracción y la reutilización
        //de código en C#.

        //GET
        List<Camiones_DTO> GetCamiones();

        //GETbyID
        Camiones_DTO GetCamion(int id);

        //INSERT (POST)
        string InsertCamion(Camiones_DTO camion);

        //UPDATE (PUT)
        string UpdateCamion(Camiones_DTO camion);

        //DELETE (DELETE)
        string DeleteCamion(int id);
    }


    //la clase que impementa lainterfaz y declara la implementación de la lógica de los métodos existentes

    public class CamionesService : ICamiones
    {
        //variables para crear el contexto (Inyección de dependencias)
        private readonly TransportesContext _context;

        //contructor par ainicializar el contexto
        public CamionesService(TransportesContext context)
        {
            _context = context;
        }

        //IMplementación de métodos

        public string DeleteCamion(int id)
        {
            try
            {
                //Obtengo primeor el Camión de la Base de datos
                Camiones _camion = _context.Camiones.Find(id);
                //valido que realmente recuperé mi objeto
                if (_camion == null)
                {
                    return $"No se encontró algún objeto con identificador {id}";
                }

                //remuevo el objeto del contexto
                _context.Camiones.Remove(_camion);
                //impacto la BD
                _context.SaveChanges();
                //respondo
                return $"Camión {id} eliminado con éxito";

            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public Camiones_DTO GetCamion(int id)
        {
            Camiones origen = _context.Camiones.Find(id);
            //DynamicMapper
            Camiones_DTO resultado = DynamicMapper.Map<Camiones, Camiones_DTO>(origen);
            return resultado;
        }

        public List<Camiones_DTO> GetCamiones()
        {
            try
            {
                //Lista de camiones del original
                List<Camiones> lista_original = _context.Camiones.ToList();
                //lista de DTOS
                List<Camiones_DTO> lista_salida = new List<Camiones_DTO>();
                //recorro cada camión y genero un nuevo DTO con DinamycMapper
                foreach (var cam in lista_original)
                {
                    //usamos el dinamycmapper para convertir los objetos
                    Camiones_DTO DTO = DynamicMapper.Map<Camiones, Camiones_DTO>(cam);
                    lista_salida.Add(DTO);
                }
                //retorno la lista con los objetos ya mapeados
                return lista_salida;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public string InsertCamion(Camiones_DTO camion)
        {
            try
            {
                //creo un camión del modelos original
                Camiones _camion = new Camiones();
                //asigno los valores del Objeto DTO del parámetro al objeto del modelo original
                _camion = DynamicMapper.Map<Camiones_DTO, Camiones>(camion);
                //añadimos el objeto al contexto
                _context.Camiones.Add(_camion);
                //impactamos la BD
                _context.SaveChanges();
                //respondo
                return "Camión insertado con éxito";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string UpdateCamion(Camiones_DTO camion)
        {
            try
            {
                //creo un camión del modelos original
                Camiones _camion = new Camiones();
                //asigno los valores del Objeto DTO del parámetro al objeto del modelo original
                _camion = DynamicMapper.Map<Camiones_DTO, Camiones>(camion);
                //modifico el estado del objeto en el contexto
                _context.Entry(_camion).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                //impactamos la BD
                _context.SaveChanges();
                //respondo
                return $"Camión {_camion.ID_Camion} Actualizado con éxito";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}

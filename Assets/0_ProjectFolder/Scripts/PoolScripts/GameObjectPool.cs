using UnityEngine;
using System.Collections.Generic; //librería para usar las Listas
using System.Linq;// Filtros de búsqueda para lista.

public class GameObjectPool : MonoBehaviour
{
    //el prefab del objeto a "poolear"
    public GameObject gameObjectToPool;

    //La lista que sirve de pool para guardar los objetos.
    private List<GameObject> pool = new List<GameObject>();

    //El tamaño inicial del pool.
    public int poolDefaultSize = 10;


    void Start()
    {
        //inicializar el pool
        for (int i = 0; i < poolDefaultSize; i++)
        {
            InstancePoolObject();
        }
    }

    private GameObject InstancePoolObject()
    {
        //Crear el objeto en la escena
        GameObject newGameobject = Instantiate(gameObjectToPool);

        //Añadir el objeto al pool/lista
        pool.Add(newGameobject);
        newGameobject.SetActive(false);
        return newGameobject;

    }

    public GameObject GetGameObjectFromPool()
    {
        //Buscar el primer objeto que esté desactivado
        GameObject target = pool.FirstOrDefault(gameObject => !gameObject.activeSelf);

        //Si no hay objetos apagados en el pool, crear y añadir uno
        if(target == null)
        {
            target = InstancePoolObject();
        }

        target.SetActive(true);
        return target;
    }

    //Función sobrecargada GetGameObjectFromPool que permite iondicar la po´sición y rotación
    public GameObject GetGameObjectFromPool(Vector3 postion, Quaternion rotation)
    {
        GameObject target = GetGameObjectFromPool();
        target.transform.position = postion;
        target.transform.rotation = rotation;
        return target;
    }







}

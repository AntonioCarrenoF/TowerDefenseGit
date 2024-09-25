using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoBase : MonoBehaviour, IAtacable, IAtacante
{
    public GameObject objetivo;
    public int vida = 100;
    public Animator anim;
    public int _dano = 5;
    public int recursosGanados = 200;
    public AdminJuego referenciaAdminJuego;
    public SpawnerEnemigos referenciaSpawner;


    private void OnEnable()
    {
        objetivo = GameObject.Find("Objetivo");
        referenciaAdminJuego = GameObject.Find("AdminJuego").GetComponent<AdminJuego>();
        referenciaSpawner = GameObject.Find("SpawnerEnemigos").GetComponent<SpawnerEnemigos>();
        objetivo.GetComponent<Objetivo>().EnObjetivoDestruido += Detener;
    }


    private void OnDisable()
    {
        objetivo.GetComponent<Objetivo>().EnObjetivoDestruido -= Detener;
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<NavMeshAgent>().SetDestination(objetivo.transform.position);
        anim = GetComponent<Animator>();
        anim.SetBool("IsMoving", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (vida <= 0)
        {
            anim.SetTrigger("OnDeath");
            GetComponent<NavMeshAgent>().SetDestination(transform.position);
            Destroy(gameObject, 3);
        }
    }

    public virtual void OnDestroy()
    {
        referenciaAdminJuego.ModificarRecursos(recursosGanados);
        referenciaSpawner.EnemigosGenerados.Remove(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Objetivo")
        {
            anim.SetBool("IsMoving", false);
            anim.SetTrigger("OnObjectiveReached");

        }
    }

    private void Detener()
    {
        anim.SetTrigger("OnObjectiveDestroyed");
        GetComponent<NavMeshAgent>().SetDestination(transform.position);
    }

    public void Danar(int dano)
    {
        if (dano == 0) dano = _dano;
        objetivo?.GetComponent<Objetivo>().RecibirDano(40);
    }

    public void RecibirDano(int dano = 5)
    {
        vida -= dano;
    }

}

using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCapacity : MonoBehaviour
{
    [SerializeField]
    private CharacterMain _characterMain;

    public TurnManager _turnManager;

    public MapMain _map;

    [field: SerializeField]
    public Capacity Capacity { get; private set; }

    private bool _hasAttacked = false;
    private bool _hasMoved = false;
    private bool _hasSpecial = false;

    /// <summary>
    /// Évènement déclenché lorsque les PA d'un personnage change.
    /// </summary>
    public event Action<int> OnPAChanged;

    public void Start()
    {
        _turnManager.OnPlayerTurn += ResetCharacter;
    }

    /// <summary>
    /// Réinitialise le personnage.
    /// </summary>
    public void ResetCharacter()
    {
        _characterMain.PaCurrent = _characterMain.PaMax;
        _hasAttacked = false;
        _hasMoved = false;
        _hasSpecial = false;
    }

    /// <summary>
    /// Utilise l'attaque normale du personnage.
    /// </summary>
    /// <param name="target">Cible de l'attaque (MonsterMain).</param>
    public void Attack(MonsterMain target)
    {
        // Attack with normal attack
        if (_hasAttacked)
        {
            return;
        }

        if (this._characterMain.PaCurrent > 0)
        {
            List<WayPoint> path = _map.aStar.GiveThePath(_characterMain.Position, target.Position);
            if (path.Count - 1 <= this._characterMain.Range)
            {
                target.MonsterHealth.TakeDamage(_characterMain.Atk);
                this._characterMain.PaCurrent--;
                OnPAChanged.Invoke(this._characterMain.PaCurrent);
                _hasAttacked = true;
            }
        }
    }

    /// <summary>
    /// Déplace le personnage.
    /// </summary>
    /// <param name="destination">Destination du personnage.</param>
    public void Move(WayPoint destination)
    {
        if (_hasMoved)
        {
            return;
        }

        if (destination.obstacle)
        {
            return;
        }

        if (this._characterMain.PaCurrent > 0)
        {
            List<WayPoint> path = _map.UseAStar(_characterMain.Position, destination);
            if (path.Count <= this._characterMain.PaCurrent)
            {
                _characterMain.Position.obstacle = false;
                for (int i = 0; i < path.Count; i++)
                {
                    ChangeWaypoint(path[i]);
                    this._characterMain.PaCurrent--;
                    OnPAChanged.Invoke(this._characterMain.PaCurrent);
                    if (i == path.Count - 1)
                    {
                        path[i].obstacle = true;
                        path[i].entity = _characterMain;
                    }
                }

                _hasMoved = true;
            }
        }
    }

    /// <summary>
    /// Change le waypoint du personnage.
    /// </summary>
    /// <param name="waypointToMoveTo">Waypoint d'arrivée.</param>
    public void ChangeWaypoint(WayPoint waypointToMoveTo)
    {
        this._characterMain.Position = waypointToMoveTo;
        this.transform.position = waypointToMoveTo.transform.position;
    }

    /// <summary>
    /// Utilise la capacité spéciale du personnage.
    /// </summary>
    /// <param name="target">Cible de la compétence spéciale (Entity).</param>
    public void Special(Entity target)
    {
        if (_hasSpecial)
        {
            return;
        }

        if (Capacity.isHealing)
        {
            if (target is CharacterMain tmp)
            {
                if (this._characterMain.PaCurrent > 0)
                {
                    tmp.CharacterHealth.HealHealth(Capacity.damage);
                    this._characterMain.PaCurrent -= Capacity.cost;
                    OnPAChanged.Invoke(this._characterMain.PaCurrent);
                    _hasSpecial = true;
                }
            }
        }
        else if (Capacity.isShielding)
        {
            if (this._characterMain.PaCurrent > 0)
            {
                _characterMain.Def += Capacity.damage;
                this._characterMain.PaCurrent -= Capacity.cost;
                OnPAChanged.Invoke(this._characterMain.PaCurrent);
                _hasSpecial = true;
            }
        }
        else
        {
            if (target is MonsterMain tmp)
            {
                if (this._characterMain.PaCurrent > 0)
                {
                    tmp.MonsterHealth.TakeDamage(Capacity.damage);
                    this._characterMain.PaCurrent -= Capacity.cost;
                    OnPAChanged.Invoke(this._characterMain.PaCurrent);
                    _hasSpecial = true;
                }
            }
        }
    }
}

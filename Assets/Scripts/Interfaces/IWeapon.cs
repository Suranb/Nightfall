using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
  public interface IWeapon : IInteractable
  {
    public void Attack(Animator playerAnimator);
    public bool CanAttack();
  }
}

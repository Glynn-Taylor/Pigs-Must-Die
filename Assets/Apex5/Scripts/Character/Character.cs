using UnityEngine;
using System.Collections;
/* Author: 	Glynn Taylor (Apex5, Crazedfish, RUI)
 * Date: 	October 2013
 * Purpose: Base class for objects that have health
 * Usage: 	Zero attribution required on builds, please attribute in code when releasing source
 *			code (when not heavily modified).
 */
public abstract class Character : MonoBehaviour {
	//Health of object
	public float Health;
	//Max health
	public float MaxHealth;
	
	//Handles taking damage and checking if alive
	public void DamageHealth(float amount){
		Health-=amount;
		if(amount>0)
			OnHurt();
		if(Health<=0)
			CharacterDeath();
	}
	//Abstract methods for doing things on death and hurt
	public abstract void CharacterDeath();
	public abstract void OnHurt();
}


using UnityEngine;
public class PowerUpController : MonoBehaviour

{
    public void Spawn(ContactPoint2D contactPoint){

        PowerUp PowerUpPrefab = Resources.Load<PowerUp>("Prefabs/PowerUp");
        PowerUp PowerUp = Instantiate<PowerUp>(PowerUpPrefab, transform);
    
        PowerUp.transform.position = contactPoint.point;

        float random = Random.value;

        PowerUp.ApplyPowerUpSprite(random);

    }
    void Update() {
          
    }
}
    

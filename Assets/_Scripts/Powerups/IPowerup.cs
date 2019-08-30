using System.Collections;
public interface IPowerup
{
	void Pickup();
	IEnumerator Despawn(float a);

}
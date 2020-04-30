using System.Collections.Generic;
using System.Linq;

public class ShootingSpeed
{
	public static readonly ShootingSpeed SLOWEST = new ShootingSpeed("SLOWEST", 0.5f), 
		SLOW = new ShootingSpeed("SLOW", 0.25f),
		FAST = new ShootingSpeed("FAST", 0.20f),
		FASTER = new ShootingSpeed("FASTER", 0.12f),
		FASTEST = new ShootingSpeed("FASTEST", 0.0625f);

	public static IEnumerable<ShootingSpeed> Values
	{
		get
		{
			yield return SLOWEST;
			yield return SLOW;
			yield return FAST;
			yield return FASTER;
			yield return FASTEST;
		}
	}

	private readonly string name;
	public float Speed { get; private set; }

	private ShootingSpeed(string name, float value)
	{
		this.name = name;
		Speed = value;
	}

	public override string ToString()
	{
		return name;
	}

	public static ShootingSpeed ValueOf(string str)
	{
		return Values.FirstOrDefault(s => s.name.Equals(str));
	}
}
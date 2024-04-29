public interface IHealth
{
    int Current { get;  }
    int Max { get;  }

    void Set(int value);
    void Heal(int value);
    void Damage(int value, float knockback);


}

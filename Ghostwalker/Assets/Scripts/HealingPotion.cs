public class HealingPotion : IPotion
{
    public Player ObjPlayer;

    public HealingPotion(Player player)
    {
        ObjPlayer = player;
    }
    
    public void Drink()
    {
        ObjPlayer.TakeDamage(-5f);
    }
}
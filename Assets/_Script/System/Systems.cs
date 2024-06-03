public class Systems : PersistentSingleton<Systems>
{
    //connet to Systems
    private AudioSystem audioSystem => Singleton<AudioSystem>.Instance;
    private ResourceSystem resourceSystem => Singleton<ResourceSystem>.Instance;
    private TimerSystem timerSystem => Singleton<TimerSystem>.Instance;
    private InventorySystem inventorySystem => Singleton<InventorySystem>.Instance;
    private HealthSystem healthSystem => Singleton<HealthSystem>.Instance;

    //flags
    public bool isInit = false;

}

public class BrokenBridge : TriggerScript
{


    public override void trigger() {
        gameObject.GetComponent<SwitchSprites>().Switch();
    }
}

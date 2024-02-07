

public class Pistol : Gunn
{
    public override void HighLight()
    {
        base.HighLight();
        _renderer.material = Constants.instance.highLightInteractable;
    }

    public override void UnHighLight()
    {
        base.UnHighLight();
        _renderer.material = Constants.instance.unhighlightInteractable;
    }
}

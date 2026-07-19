using UnityEngine;

public class MetalBlockController : BlockController
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        AudioManager.instance.PlayAudio(SoundType.ImpactHeavy);
    }
}

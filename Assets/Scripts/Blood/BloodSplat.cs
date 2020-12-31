using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplat : MonoBehaviour
{
    public enum SplatLocation { 
        Foreground,
        Background,
    }

    public Color backgroundTint;
    public float minSizeMod = 0.8f;
    public float maxSizeMode = 1.5f;

    public Sprite[] sprites;
    private SplatLocation splatLocation;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Initialize(SplatLocation splatLocation)
    {
        this.splatLocation = splatLocation;
        SetSprite();
        SetRotation();
        SetProperties();
        SetSize();
    }
    void SetSprite() {
        var index = Random.Range(0, sprites.Length);
        spriteRenderer.sprite = sprites[index];
    }
    void SetSize() {
        float sizeMode = Random.Range(minSizeMod, maxSizeMode);
        transform.localScale *= sizeMode;
    }
    void SetRotation()
    {
        float angle = Random.Range(-360, 360f);
        this.transform.rotation = Quaternion.Euler(0,0,angle);


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground")|| collision.gameObject.tag.Equals("Spikes")) {
            StartCoroutine(StandBy());
        }
    }
   
    IEnumerator StandBy() {
        yield return new WaitForSeconds(Random.Range(0f, 0.1f));
        if (GetComponent<Rigidbody2D>() == null) yield return true;
        if (GetComponent<Rigidbody2D>() != null)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Destroy(GetComponent<Rigidbody2D>());
        }
    }
    void SetProperties() {

        if (splatLocation == SplatLocation.Background) {
            spriteRenderer.color = backgroundTint;
            spriteRenderer.sortingOrder = 0;
        }

        if (splatLocation == SplatLocation.Foreground)
        {
         //   spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
           // spriteRenderer.sortingOrder = 3;
        }
    }

}

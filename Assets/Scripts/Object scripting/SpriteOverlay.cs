using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOverlay : MonoBehaviour
{
    [SerializeField]
    GameObject Heart, Cursor, OverlayHealth, OverlayCursor, Camera;

    [SerializeField]
    float SpacingY, SpacingX, OffsetY, OffsetX;

    public void RemoveHeart()
    {
        removeSprite(OverlayHealth);
    }

    public void RemoveKlick()
    {
        removeSprite(OverlayCursor);
    }

    void removeSprite(GameObject spriteContainer)
    {
        Transform[] sprites = spriteContainer.transform.GetComponentsInChildren<Transform>();
        if (sprites.Length - 1 != 0) Destroy(sprites[sprites.Length - 1].gameObject);
    }

    public void MakeClickSpriteUI(int MaxClicks)
    {
        SpawnSprites(Cursor, OverlayCursor, SpacingX, MaxClicks);
    }

    public void MakeHealthSpriteUI(int MaxHealth)
    {
        SpawnSprites(Heart, OverlayHealth, SpacingX, MaxHealth);
    }

    void ClearSprites(GameObject spriteContainer)
    {
        Transform[] sprites = spriteContainer.transform.GetComponentsInChildren<Transform>();

        for (int i = 1; i < sprites.Length; i++)
        {
            Destroy(sprites[i].gameObject);
        }
    }

    void SpawnSprites(GameObject sprite, GameObject overlay, float spacing, int amount)
    {
        ClearSprites(overlay);
        for (int i = 0; i < amount; i++) 
        {
            GameObject newSprite = Instantiate(sprite, new Vector3(transform.position.x + i * spacing, overlay.transform.position.y, transform.position.z), Quaternion.identity);
            newSprite.transform.parent = overlay.transform;
            newSprite.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void FixPos()
    {
        Camera cam = Camera.GetComponent<Camera>();
        Vector2 p = cam.ScreenToWorldPoint(new Vector2(0, cam.pixelHeight));
        transform.position = new Vector3(p.x + OffsetX, p.y + OffsetY, -0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        FixPos();
        //TODO: Fix function for changing screen size. Like, one. For everything. In main game, there are now 3 different scripts looking for screen size changes. 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "VinTools/Custom Tiles/Mask Rule Tile")]
public class MaskRuleTile : RuleTile<MaskRuleTile.Neighbor> 
{
    public GameObject maskPrefab;

    public class Neighbor : RuleTile.TilingRule.Neighbor {

    }

    public override bool RuleMatch(int neighbor, TileBase tile) {
        switch (neighbor) {
            case Neighbor.This: return tile == this;
            case Neighbor.NotThis: return tile != this;
        }
        return base.RuleMatch(neighbor, tile);
    }

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        base.RefreshTile(position, tilemap);
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);

        GameObject go = new GameObject("Mask");
        go.AddComponent<SpriteMask>().sprite = tileData.sprite;
        tileData.gameObject = go;

#if UNITY_EDITOR
        if (UnityEditor.EditorApplication.isPlaying) Destroy(go, .01f);
        else DestroyImmediate(go);
#else
        Destroy(go, .01f);
#endif

        /*maskPrefab.GetComponent<SpriteMask>().sprite = tileData.sprite;
        tileData.gameObject = maskPrefab;*/
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimation : MonoBehaviour {

    public enum AnimationType
    {
        Idle,
        Walk,
        Attack
    };

    public Texture2D spriteTex;
    List<Sprite> idleSprites = new List<Sprite>();
    List<Sprite> walkSprites = new List<Sprite>();
    List<Sprite> attackSprites = new List<Sprite>();

    SpriteRenderer spriteRenderer;

    int spriteIdx = 0;
    Coroutine corunite;

    bool isPlaying = false;
    public void Init(Card card)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteTex = Resources.Load<Texture2D>(string.Format("Cards/{0}/{1}_sheet", card.ImageName, card.ImageName));

        var unitAnimationInfo = Data.ReadUnitAnimationInfo();
        foreach (var info in unitAnimationInfo)
        {
            if (info.UnitID == card.ID)
            {
                CreateAnimations(info);
                break;
            }
        }

        PlayIdleAnimation();
    }

    private void CreateAnimations(UnitAnimationInfo info)
    {
        //Load Texture

        //Create idle anim
        CreateAnimation(info.IdleInfo.Lenght, info.IdleInfo.Size, info.IdleInfo.Start, spriteTex, idleSprites);

        //Create walk anim
        CreateAnimation(info.WalkInfo.Lenght, info.WalkInfo.Size, info.WalkInfo.Start, spriteTex, walkSprites);

        //Create attack anim
        CreateAnimation(info.AttackInfo.Lenght, info.AttackInfo.Size, info.AttackInfo.Start, spriteTex, attackSprites);

    }
    private void CreateAnimation(int lenght, int size, int startY, Texture2D tex, List<Sprite> spriteList)
    {
        var pivot = new Vector2(0.5f, 0.05f);
        if (size == 192)
            pivot = new Vector2(0.5f, 0.35f);

        for (int i = 0; i < lenght; i++)
        {
            var sprite = Sprite.Create(tex, new Rect(size*i, startY, size, size), pivot);
            spriteList.Add(sprite);
        }
    }

    private IEnumerator PlayAnimation(List<Sprite> sprites, bool loop)
    {
        isPlaying = true;
        while (true)
        {
            spriteRenderer.sprite = sprites[spriteIdx];
            yield return new WaitForSeconds(0.05f);

            if (CountIndex(sprites.Count) && !loop) break;
        }
        isPlaying = false;
    }
    public void PlayIdleAnimation()
    {
        if(corunite != null) StopCoroutine(corunite);
        spriteIdx = 0;
        corunite = StartCoroutine(PlayAnimation(idleSprites, true));
    }
    public void PlayWalkAnimation()
    {
        if (corunite != null) StopCoroutine(corunite);
        spriteIdx = 0;
        corunite = StartCoroutine(PlayAnimation(walkSprites, true));
    }
    public IEnumerator PlayAttackAnimation()
    {
        if (corunite != null) StopCoroutine(corunite);
        spriteIdx = 0;
        corunite = StartCoroutine(PlayAnimation(attackSprites, false));
        yield return corunite;
        PlayIdleAnimation();
    }
    private bool CountIndex(int maxCount)
    {
        if (spriteIdx == maxCount - 1){
            spriteIdx = 0;
            return true;
        }
        else
            spriteIdx++;

        return false;
    }
}

public struct UnitAnimationInfo
{
    public int UnitID;
    public AnimInfo IdleInfo;
    public AnimInfo WalkInfo;
    public AnimInfo AttackInfo;

    public UnitAnimationInfo(int unitID, AnimInfo idle, AnimInfo walk, AnimInfo attack)
    {
        UnitID = unitID;
        IdleInfo = idle;
        WalkInfo = walk;
        AttackInfo = attack;
    }
}
public struct AnimInfo
{
    public int Size;
    public int Start;
    public int Lenght;

    public AnimInfo(int size, int lenght, int start)
    {
        Size = size;
        Start = start;
        Lenght = lenght;
    }
}

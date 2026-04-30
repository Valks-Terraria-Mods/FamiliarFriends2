using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace FamiliarFriends.NPCs;

public class Friend : ModNPC
{
    public Player Owner { get; set; }
    public Player Sprite { get; set; }

    public override void OnSpawn(IEntitySource source)
    {
        base.OnSpawn(source);
        NPC.GivenName = "Tester";
    }

    public override void SetStaticDefaults()
    {
        FriendDefaults.ApplyStatic(Type);
    }

    public override void SetDefaults()
    {
        FriendDefaults.Apply(NPC, this);
    }

    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        Sprite.position = new Vector2(
            NPC.Center.X - (Sprite.width * 0.5f),
            NPC.Bottom.Y - Sprite.height
        );

        Sprite.direction = NPC.direction;
        Sprite.velocity = NPC.velocity;
        Sprite.gfxOffY = NPC.gfxOffY;
        Sprite.PlayerFrame();
        Main.PlayerRenderer.DrawPlayer(Main.Camera, Sprite, Sprite.position, Sprite.fullRotation, Sprite.fullRotationOrigin);
        return false;
    }

    public override void AI()
    {
        var config = ModContent.GetInstance<FriendAiConfig>();

        Vector2 ownerPos = Owner.position + new Vector2(Owner.width * 0.5f, Owner.height * 0.5f);
        Vector2 dir = NPC.DirectionTo(ownerPos);

        // The friend is too far and needs to close the distance to the player
        if (NPC.Distance(ownerPos) >= config.MinFollowDistance)
        {
            NPC.direction = dir.X > 0 ? 1 : -1;
            NPC.velocity.X = float.Lerp(NPC.velocity.X, dir.X * config.MaxWalkSpeed, config.WalkAcceleration);
        }
        else
        {
            // The friend has reached the player and needs to slow down
            if (Math.Abs(NPC.velocity.X) >= config.MinWalkSpeed)
            {
                NPC.velocity.X = float.Lerp(NPC.velocity.X, 0, config.WalkDeacceleration);
            }
            else
            {
                NPC.velocity.X = 0;
            }
        }
    }

    public override bool NeedSaving()
    {
        return false;
    }

    private static class FriendDefaults
    {
        public static void ApplyStatic(int type)
        {
            Main.npcFrameCount[type] = 25;
            NPCID.Sets.ExtraFramesCount[type] = 9;
            NPCID.Sets.AttackFrameCount[type] = 4;
            NPCID.Sets.DangerDetectRange[type] = 700;
            NPCID.Sets.AttackType[type] = 0;
            NPCID.Sets.AttackTime[type] = 90;
            NPCID.Sets.AttackAverageChance[type] = 30;
            NPCID.Sets.HatOffsetY[type] = 4;
            NPCID.Sets.ShimmerTownTransform[type] = true;
            NPCID.Sets.DoesntDespawnToInactivityAndCountsNPCSlots[type] = true;
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new()
            {
                Velocity = 1f,
                Direction = 1
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(type, drawModifiers);
            ContentSamples.NpcBestiaryRarityStars[type] = 3;
        }

        public static void Apply(NPC npc, Friend friend)
        {
            npc.townNPC = false;
            npc.friendly = true;
            npc.timeLeft = int.MaxValue;
            npc.width = 18;
            npc.height = 40;
            npc.aiStyle = -1;
            npc.damage = 10;
            npc.defense = 15;
            npc.lifeMax = 250;
            npc.stepSpeed = 1;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
            friend.AnimationType = NPCID.Guide;
        }
    }
}

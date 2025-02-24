using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Text_Table
{
    public class AugText_Table
    {
        //0 ~ MAX
        private int augsLevel = 4;

        #region 워리어
        public void Two_Hand_Sword(ref List<string> strings)
        {

            for (int i = 0; i < augsLevel; i++)
            {
                switch (i)
                {
                    case 0:
                        strings.Add("전방에 검기를 날립니다.");
                        break;
                    case 1:
                        strings.Add("검기의 크기가 증가합니다.");
                        break;
                    case 2:
                        strings.Add("검기를 두 방향으로 날립니다.");
                        break;
                    case 3:
                        strings.Add("검기를 더 빨리 사용합니다.");
                        break;
                    case 4:
                        strings.Add("검기를 세방향으로 날려보내며, 검기가 적의 투사체를 막아냅니다.");
                        break;
                }
            }

        }
        public void Big_Sword(ref List<string> strings)
        {
            for (int i = 0; i < augsLevel; i++)
            {
                switch (i)
                {
                    case 0:
                        strings.Add("캐릭터를 중심으로 땅을 강하게 내리쳐 피해를 입힙니다.");
                        break;
                    case 1:
                        strings.Add("지진의 범위가 증가합니다.");
                        break;
                    case 2:
                        strings.Add("지진을 더 빨리 사용합니다.");
                        break;
                    case 3:
                        strings.Add("지진의 강도가 증가합니다.");
                        break;
                    case 4:
                        strings.Add("지진의 범위가 더욱 넓어지며, 여진이 추가됩니다.");
                        break;
                }
            }
        }

        public void Sword_Shield(ref List<string> strings)
        {

            for (int i = 0; i < augsLevel; i++)
            {
                switch (i)
                {
                    case 0:
                        strings.Add("적의 공격을 방어하며, 투사체라면 투사체를 튕겨내고 맞은 적에게 데미지를 가합니다. 사용 후 일정시간이 지나야 재사용 가능합니다.");
                        break;
                    case 1:
                        strings.Add("조금 더 빨리 방어 태세에 들어갑니다.");
                        break;
                    case 2:
                        strings.Add("방어 시 잠시간 피해를 받지 않으며, 이동 속도가 증가합니다.");
                        break;
                    case 3:
                        strings.Add("방패가 조금 더 두꺼워집니다.");
                        break;
                    case 4:
                        strings.Add("피해를 받지 않는 시간이 증가하며, 이동 속도가 큰폭으로 증가합니다.");
                        break;
                }
            }
        }
        public void Shielder(ref List<string> strings)
        {
            for (int i = 0; i < augsLevel; i++)
            {
                switch (i)
                {
                    case 0:
                        strings.Add("캐릭터가 받는 피해가 감소하며, 대쉬 사용 시 전방으로 돌진하여 경로에 있는 적에게 피해를 가합니다.");
                        break;
                    case 1:
                        strings.Add("대쉬를 조금 더 자주 사용할 수 있습니다.");
                        break;
                    case 2:
                        strings.Add("방패가 조금 더 단단해지며, 대쉬 회수가 증가합니다.");
                        break;
                    case 3:
                        strings.Add("대쉬를 더 자주 사용할 수 있습니다.");
                        break;
                    case 4:
                        strings.Add("검기가 적의 투사체를 막아냅니다.");
                        break;
                }
            }
        }
        #endregion
        #region 아처
        public void Long_Bow(ref List<string> strings)
        {

            for (int i = 0; i < augsLevel; i++)
            {
                switch (i)
                {
                    case 0:
                        strings.Add("기본 공격 시, 투사체가 1개 추가됩니다. 기본 공격에 사거리 제한이 사라집니다.");
                        break;
                    case 1:
                        strings.Add("");
                        break;
                    case 2:
                        strings.Add("");
                        break;
                    case 3:
                        strings.Add("");
                        break;
                    case 4:
                        strings.Add("");
                        break;
                }
            }
        }
        public void Cross_Bow(ref List<string> strings)
        {
            for (int i = 0; i < augsLevel; i++)
            {
                switch (i)
                {
                    case 0:
                        strings.Add("전방에 빠르게 화살을 쏘아냅니다.");
                        break;
                    case 1:
                        strings.Add("");
                        break;
                    case 2:
                        strings.Add("");
                        break;
                    case 3:
                        strings.Add("");
                        break;
                    case 4:
                        strings.Add("");
                        break;
                }
            }
        }
        public void Great_Bow(ref List<string> strings)
        {
            for (int i = 0; i < augsLevel; i++)
            {
                switch (i)
                {
                    case 0:
                        strings.Add("전방에 강력한 화살을 발사합니다.");
                        break;
                    case 1:
                        strings.Add("");
                        break;
                    case 2:
                        strings.Add("");
                        break;
                    case 3:
                        strings.Add("");
                        break;
                    case 4:
                        strings.Add("");
                        break;
                }
            }
        }
        public void Arc_Ranger(ref List<string> strings)
        {
            for (int i = 0; i < augsLevel; i++)
            {
                switch (i)
                {
                    case 0:
                        strings.Add("대쉬 사용 시, 전방의 부채꼴 방면으로 화살을 퍼트리듯 발사하며 이동합니다.");
                        break;
                    case 1:
                        strings.Add("");
                        break;
                    case 2:
                        strings.Add("");
                        break;
                    case 3:
                        strings.Add("");
                        break;
                    case 4:
                        strings.Add("");
                        break;
                }
            }
        }
        #endregion
        #region 매지션
        public void Staff(ref List<string> strings)
        {
            for (int i = 0; i < augsLevel; i++)
            {
                switch (i)
                {
                    case 0:
                        strings.Add("필드의 모든 몬스터를 대상으로 강한 공격을 가합니다.");
                        break;
                    case 1:
                        strings.Add("");
                        break;
                    case 2:
                        strings.Add("");
                        break;
                    case 3:
                        strings.Add("");
                        break;
                    case 4:
                        strings.Add("");
                        break;
                }
            }
        }
        public void Wand(ref List<string> strings)
        {
            for (int i = 0; i < augsLevel; i++)
            {
                switch (i)
                {
                    case 0:
                        strings.Add("메인 특성의 쿨타임을 주기적으로 초기화합니다.");
                        break;
                    case 1:
                        strings.Add("");
                        break;
                    case 2:
                        strings.Add("");
                        break;
                    case 3:
                        strings.Add("");
                        break;
                    case 4:
                        strings.Add("");
                        break;
                }
            }
        }
        public void Orb(ref List<string> strings)
        {
            for (int i = 0; i < augsLevel; i++)
            {
                switch (i)
                {
                    case 0:
                        strings.Add("자기를 중심으로 일정 범위 내 피해를 주는 화염구를 소환합니다.");
                        break;
                    case 1:
                        strings.Add("");
                        break;
                    case 2:
                        strings.Add("");
                        break;
                    case 3:
                        strings.Add("");
                        break;
                    case 4:
                        strings.Add("");
                        break;
                }
            }
        }
        public void Warlock(ref List<string> strings)
        {
            for (int i = 0; i < augsLevel; i++)
            {
                switch (i)
                {
                    case 0:
                        strings.Add("대쉬가 텔레포트로 변경됩니다.");
                        break;
                    case 1:
                        strings.Add("");
                        break;
                    case 2:
                        strings.Add("");
                        break;
                    case 3:
                        strings.Add("");
                        break;
                    case 4:
                        strings.Add("");
                        break;
                }
            }
        }
        #endregion
    }
}
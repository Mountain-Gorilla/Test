using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIDirector : MonoBehaviour
{
    public GameObject g_OriginalUI;                                    //表示したいUI元

    private List<GameObject> g_EnemyList=new List<GameObject>();                                     //エネミー取得用
    private List<EnemyState> es_enemyStates=new List<EnemyState>();    //各エネミーのステータスクラス取得用

    private List<GameObject> g_EnemyUI=new List<GameObject>();         //各エネミーのUI保存先
    private List<Image> im_images=new List<Image>();                   //各エネミーのUIイメージクラス取得用

    // 初期化
    void Start()
    {
        //リスト初期化

        //エネミー取得
        GameObject[] EnemyArray = GameObject.FindGameObjectsWithTag("Enemys");
        //リストに移し替える
        g_EnemyList.AddRange(EnemyArray);


        //取得したエネミーごとにUIを作成
        foreach(GameObject enemy in g_EnemyList)
        {
            //エネミーのステータス取得
            es_enemyStates.Add(enemy.GetComponent<EnemyState>());
            
            //エネミーのUIを保存
            g_EnemyUI.Add(Instantiate(g_OriginalUI) as GameObject);
            
        }

        //UI毎の処理
        foreach(GameObject enemyUI in g_EnemyUI)
        {
            //UIのイメージクラスを保存
            im_images.Add(enemyUI.transform.Find("EnemyHP_Front").GetComponent<Image>());
            //UI元は非アクティブなのでアクティブに切替
            enemyUI.SetActive(true);
        }
    }



    // 更新
    void Update()
    {
        int cnt = 0;
        //エネミー削除の確認
        foreach (EnemyState state in es_enemyStates)
        {
            //中身がないとき
            if (!state.b_Alive)
            {
                Destroy(g_EnemyUI[cnt]);
                g_EnemyUI.RemoveAt(cnt);
                g_EnemyList.RemoveAt(cnt);
                es_enemyStates.RemoveAt(cnt);
                im_images.RemoveAt(cnt);
                break;
            }
            cnt++;
        }

        cnt = 0;
        foreach (GameObject enemy in g_EnemyList)
        {
            //UIの位置をエネミーに合わせる（被るので1.5fかけてるけど後で直す）
            Vector3 pos = enemy.transform.position;
            pos.y += 1.5f;
            g_EnemyUI[cnt].transform.position = pos;
            //カウントアップ
            cnt++;
        }
    }

    //最後に更新
    private void LateUpdate()
    {
        //HPに応じてHPバー減少
        //カウントリセット
        int cnt = 0;
        foreach (EnemyState state in es_enemyStates)
        {
            //UIイメージのfillを変更
            im_images[cnt].fillAmount = state.f_Hp / state.f_MaxHp;
            //カウントアップ
            cnt++;
        }
    }
}

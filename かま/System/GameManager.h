#pragma once

// 継承先でoverrideする関数は必ずvirtualをつける
// トップとなるクラスを基底クラス、スーパークラスという
class Object
{
protected:
	// 行列
	KdMatrix          m_mWorld;

	// オブジェクト名保存用
	std::string m_Name = "No Name";

public:

	std::string GetName() { return m_Name; }
	void SetName(const std::string _name) { m_Name = _name; }

	// 仮想デストラクタ(今後コメントは書かないこと)
	virtual ~Object(){}

	// 更新処理
	virtual void Update(){}

	// 描画処理
	virtual void Draw(){}

	// 行列をセット
	void SetMatrix(const KdMatrix& m) { m_mWorld = m; }

	// ImGuiの項目を描画
	virtual void ImGuiUpdate();

	/*==============================*/
	// デシリアライズ / シリアライズ
	/*==============================*/
	virtual void Deserialize(const json11::Json& jsonObj);

	// 自分のデータをoutJsonへ格納する
	virtual void Serialize(json11::Json::object& outJson);
};


/*================================*/
//  ゲームの主役となる部分
/*================================*/

class GameManager
{
private:
	// オブジェクトリスト
	// キャラクタを扱うときは必ずポインタで
	std::list<SPtr<Object>> m_ObjectList;

	/*=========================*/
	//  Editor(実際の制作時は別のところに用意しておくこと)
	/*=========================*/

	WPtr<Object>   m_Editor_SelectObj;

	// カメラで動かす
	
	EdiorCamera m_Editor_Camera;
public:
	// 初期設定
	void Init();

	// ゲームオブジェクト追加関数
	void AddObject(SPtr<Object> _obj) { m_ObjectList.push_back(_obj); }

	// ゲーム処理
	// ゲームのメイン関数に近いので更新と描画を書いている
	void Run();

private :
	// ImGuiの処理
	void ImGuiDraw();

};
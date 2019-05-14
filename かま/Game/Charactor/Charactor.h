#pragma once

// Stageオブジェクトキャラ

class Chara : public Object
{
private:

	//3Dメッシュモデル
	SPtr<KdGameModel> m_model;

	// モデルファイル名
	std::string m_modelFileName;

	// ボーン操作用
	KdBoneController  m_Bone;

	// アニメータ
	KdAnimator        m_Anime;

	// HP
	int               n_Hp = 100;
public:

	// 更新処理
	virtual void Update() override;

	// 描画処理         ↓overrideするものは必ずつけること
	virtual void Draw() override;

	virtual void ImGuiUpdate() override;

	// デシリアライズ
	virtual void Deserialize(const json11::Json& jsonObj) override;

	virtual void Serialize(json11::Json::object& outJson)override;

	void SetModel(const std::string& _modelfilename );

	void SetHp(int _hp) { n_Hp = _hp; }

};
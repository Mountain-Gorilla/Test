#pragma once

// Stageオブジェクトキャラ

class StageObject: public Object
{
private:
	//3Dメッシュモデル
	SPtr<KdGameModel> m_model;
	
	// モデルファイル名
	std::string m_modelFileName;

public:

	// 初期設定関数
	void SetModel(const std::string& _modelfilename);

	// 描画処理         ↓overrideするものは必ずつけること
	virtual void Draw() override;

	// ImGuiを描画
	virtual void ImGuiUpdate() override;

	// デシリアライズ
	virtual void Deserialize(const json11::Json& jsonObj) override;

	virtual void Serialize(json11::Json::object& outJson)override;
};
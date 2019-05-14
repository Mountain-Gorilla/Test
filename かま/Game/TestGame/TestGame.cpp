#include "main.h"

#include "System/GameManager.h"

#include "TestGame.h"

void StageObject::SetModel(const std::string& _modelfilename)
{
	m_modelFileName = _modelfilename;
	m_model = KDResFactory.GetGameModel(_modelfilename);
}

void StageObject::Draw()
{
	if (m_model == nullptr)return;

	// シェーダに行列をセット
	ShMgr.m_KdModelSh.SetWorld(m_mWorld);
	
	// シェーダで描画
	ShMgr.m_KdModelSh.DrawGameModel(*m_model, nullptr);
}

void StageObject::ImGuiUpdate()
{
	Object::ImGuiUpdate();

	// 画像ファイル選択ボタン m_TexFilenameはstd::string型です。
	if (ImGuiResourceButton(u8"gazouFile", m_modelFileName, { ".xed" })) {
	    SetModel(m_modelFileName);
	}

}

void StageObject::Deserialize(const json11::Json& jsonObj)
{
	Object::Deserialize(jsonObj);

	// モデルファイル名を読み込む
	SetModel(jsonObj["ModelFileName"].string_value());
}

void StageObject::Serialize(json11::Json::object& outJson)
{
	Object::Serialize(outJson);

	outJson["ModelFileName"] = m_modelFileName;
	outJson["ClassName"] = "StageObject";
}
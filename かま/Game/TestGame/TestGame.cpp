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

	// �V�F�[�_�ɍs����Z�b�g
	ShMgr.m_KdModelSh.SetWorld(m_mWorld);
	
	// �V�F�[�_�ŕ`��
	ShMgr.m_KdModelSh.DrawGameModel(*m_model, nullptr);
}

void StageObject::ImGuiUpdate()
{
	Object::ImGuiUpdate();

	// �摜�t�@�C���I���{�^�� m_TexFilename��std::string�^�ł��B
	if (ImGuiResourceButton(u8"gazouFile", m_modelFileName, { ".xed" })) {
	    SetModel(m_modelFileName);
	}

}

void StageObject::Deserialize(const json11::Json& jsonObj)
{
	Object::Deserialize(jsonObj);

	// ���f���t�@�C������ǂݍ���
	SetModel(jsonObj["ModelFileName"].string_value());
}

void StageObject::Serialize(json11::Json::object& outJson)
{
	Object::Serialize(outJson);

	outJson["ModelFileName"] = m_modelFileName;
	outJson["ClassName"] = "StageObject";
}
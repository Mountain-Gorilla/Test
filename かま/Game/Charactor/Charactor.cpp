#include "main.h"

#include "System\GameManager.h"

#include "Charactor.h"

void Chara::SetModel(const std::string& _modelfilename)
{
	// ����������
	m_Bone.Release();
	m_Anime.ResetTrack();

	m_modelFileName = _modelfilename;

	m_model = KDResFactory.GetGameModel(_modelfilename);

	if (m_model) {

		// m_model�p�̃{�[���ɏ���������
		m_Bone.Init(*m_model);

		// BoneController�ƃA�j���[�^��ڑ�����
		m_Bone.BindAnimator(m_Anime);

		// �A�j���[�V���������f���ɓo�^
		m_Anime.SetAnimationList(m_model->GetAnimeList());
		m_Anime.ChangeAnime("�U���P", true);  //�A�j�����A���[�v
	}
}

void Chara::Update()
{
	// �A�j���[�V�����i�s
	m_Anime.Animation(1,0,nullptr);

	// �S�{�[�����Čv�Z
	m_Bone.CalcBoneMatrix();

	m_mWorld.Move_Local(0.0f, 0.0f, 0.01f);
}

void Chara::Draw()
{
	if (m_model == nullptr)return;

	// �V�F�[�_�ɍs����Z�b�g
	ShMgr.m_KdModelSh.SetWorld(m_mWorld);

	// �V�F�[�_�ŕ`��
	ShMgr.m_KdModelSh.DrawGameModel(*m_model, &m_Bone);
}

void Chara::ImGuiUpdate()
{
	// �p�����̊֐����Ă�
	Object::ImGuiUpdate();

	ImGui::DragInt("Hp", &n_Hp, 1.0f);

	// �摜�t�@�C���I���{�^�� m_TexFilename��std::string�^�ł��B
	if (ImGuiResourceButton(u8"Model File", m_modelFileName, { ".xed" })) {
		SetModel(m_modelFileName);
	}
}

void Chara::Deserialize(const json11::Json& jsonObj) 
{
	Object::Deserialize(jsonObj);

	// ���f���t�@�C������ǂݍ���
	SetModel(jsonObj["ModelFileName"].string_value());

	// HP
	n_Hp = jsonObj["Hp"].int_value();
}

void Chara::Serialize(json11::Json::object& outJson)
{
	Object::Serialize(outJson);

	outJson["ModelFileName"] = m_modelFileName;
	outJson["ClassName"] = "Chara";

	outJson["Hp"] = n_Hp;
}